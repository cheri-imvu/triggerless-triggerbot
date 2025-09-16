/* trigger-player.js — one shared <audio> for the whole page + flexible API origin
 * Markup:
 *   <div id="product-trigger-player"
 *        data-pid="72457182"
 *        data-api-origin="https://www.triggerless.com"></div>
 *   <script src="https://triggerless.com/triggerbot/trigger-player.js"></script>
 *
 * Notes:
 * - If you omit data-api-origin, the script will default to the script’s own origin,
 *   then try the toggled www/non-www variant as a fallback.
 * - To truly work cross-site, your API must send CORS headers (e.g., ACAO:*).
 */
(() => {
  /* ---------- Resolve asset base (icons live next to this file) ---------- */
  const getAssetBase = () => {
    const scripts = document.getElementsByTagName('script');
    let me = null;
    for (const s of scripts) if (s.src && s.src.endsWith('/trigger-player.js')) { me = s; break; }
    if (!me && document.currentScript) me = document.currentScript;
    const src = me?.src || '';
    return src ? src.slice(0, src.lastIndexOf('/') + 1) : '';
  };
  const ASSET_BASE = getAssetBase();
  const PLAY_ICON = ASSET_BASE + 'play.png';
  const STOP_ICON = ASSET_BASE + 'stop.png';

  /* ---------- Styles ---------- */
  const injectStyles = () => {
    if (document.getElementById('trigger-player-styles')) return;
    const css = `
#product-trigger-player, .product-trigger-player { width: 100%; }
#product-trigger-player .tp-grid, .product-trigger-player .tp-grid {
  display: grid; grid-template-columns: repeat(8, 1fr); width: 100%;
  background: #000; gap: 1px;    /* 1px black separators */
}
#product-trigger-player .tp-cell, .product-trigger-player .tp-cell {
  background: #fff; min-height: 86px;
  display: flex; align-items: center; justify-content: center;
  padding: 10px 6px; text-align: center; box-sizing: border-box;
}
#product-trigger-player .tp-inner, .product-trigger-player .tp-inner {
  display: flex; flex-direction: column; align-items: center; gap: 6px; max-width: 100%;
}
#product-trigger-player .tp-name, .product-trigger-player .tp-name {
  font-family: system-ui, Arial, sans-serif; font-size: 13px; line-height: 1.2; color: #111; word-break: break-word;
}
#product-trigger-player .tp-btn, .product-trigger-player .tp-btn {
  display: inline-block; width: 28px; height: 28px; cursor: pointer; user-select: none;
}
#product-trigger-player .tp-btn img, .product-trigger-player .tp-btn img { width: 100%; height: 100%; display: block; }
#product-trigger-player .tp-sr, .product-trigger-player .tp-sr {
  position: absolute; width:1px; height:1px; padding:0; margin:-1px; overflow:hidden;
  clip: rect(0,0,0,0); white-space:nowrap; border:0;
}
@media (max-width: 900px){ #product-trigger-player .tp-grid, .product-trigger-player .tp-grid { grid-template-columns: repeat(6, 1fr); } }
@media (max-width: 700px){ #product-trigger-player .tp-grid, .product-trigger-player .tp-grid { grid-template-columns: repeat(4, 1fr); } }
@media (max-width: 480px){ #product-trigger-player .tp-grid, .product-trigger-player .tp-grid { grid-template-columns: repeat(2, 1fr); } }
`.trim();
    const style = document.createElement('style');
    style.id = 'trigger-player-styles';
    style.textContent = css;
    document.head.appendChild(style);
  };

  /* ---------- API origin helpers ---------- */
  const scriptOrigin = (() => {
    try { return new URL(ASSET_BASE, location.href).origin; } catch { return location.origin; }
  })();

  const toggleWwwOrigin = (origin) => {
    try {
      const u = new URL(origin);
      const hn = u.hostname;
      const nh = hn.startsWith('www.') ? hn.slice(4) : ('www.' + hn);
      return `${u.protocol}//${nh}${u.port ? ':' + u.port : ''}`;
    } catch { return origin; }
  };

  const buildApiUrl = (origin, pid) =>
    `${origin.replace(/\/+$/,'')}/api/product/sounds/${encodeURIComponent(pid)}`;

  /* ---------- Fetch with graceful fallback (www vs non-www) ---------- */
  const fetchSounds = async (pid, apiOriginHint) => {
    const primaryOrigin = apiOriginHint || scriptOrigin;
    const candidates = [primaryOrigin, toggleWwwOrigin(primaryOrigin)];

    let lastErr = null;
    for (const origin of candidates) {
      try {
        const res = await fetch(buildApiUrl(origin, pid), { mode: 'cors', credentials: 'omit' });
        if (!res.ok) throw new Error(`HTTP ${res.status} @ ${origin}`);
        return await res.json();
      } catch (e) {
        lastErr = e;
        // If this is a CORS failure, the error is typically a TypeError: Failed to fetch
        // Try next candidate.
      }
    }
    throw lastErr || new Error('Failed to fetch sounds');
  };

  /* ---------- Global shared audio (one per PAGE) ---------- */
  const getGlobalPlayer = () => {
    if (window.__TriggerlessAudio) return window.__TriggerlessAudio;

    const audio = document.createElement('audio');
    audio.preload = 'none';
    audio.style.display = 'none';
    document.body.appendChild(audio);

    const state = { audio, currentBtn: null };

    const setIcon = (btn, isPlaying) => {
      if (!btn) return;
      const img = btn.querySelector('img');
      const sr = btn.querySelector('.tp-sr');
      if (img) { img.src = isPlaying ? STOP_ICON : PLAY_ICON; img.alt = isPlaying ? 'Stop' : 'Play'; }
      btn.setAttribute('aria-pressed', String(isPlaying));
      if (sr) sr.textContent = isPlaying ? 'Stop' : 'Play';
    };

    const stop = () => {
      if (!audio.paused) { audio.pause(); audio.currentTime = 0; }
      setIcon(state.currentBtn, false);
      state.currentBtn = null;
    };

    audio.addEventListener('ended', () => { setIcon(state.currentBtn, false); state.currentBtn = null; });
    audio.addEventListener('error', () => { setIcon(state.currentBtn, false); state.currentBtn = null; console.error('Audio error', audio.error); });

    window.__TriggerlessAudio = { state, setIcon, stop };
    return window.__TriggerlessAudio;
  };

  /* ---------- DOM builders ---------- */
  const createCell = (item) => {
    const cell = document.createElement('div');
    cell.className = 'tp-cell';

    const inner = document.createElement('div');
    inner.className = 'tp-inner';

    const name = document.createElement('div');
    name.className = 'tp-name';
    name.textContent = item.Name || '(unnamed)';

    const btn = document.createElement('button');
    btn.className = 'tp-btn';
    btn.type = 'button';
    btn.setAttribute('aria-pressed', 'false');
    btn.setAttribute('aria-label', `Play ${item.Name || 'sound'}`);
    btn.title = `Play ${item.Name || 'sound'}`;
    btn.dataset.url = item.Url;

    const sr = document.createElement('span');
    sr.className = 'tp-sr';
    sr.textContent = 'Play';
    btn.appendChild(sr);

    const img = document.createElement('img');
    img.src = PLAY_ICON;
    img.alt = 'Play';
    btn.appendChild(img);

    inner.appendChild(name);
    inner.appendChild(btn);
    cell.appendChild(inner);
    return cell;
  };

  const renderWidget = (container, items) => {
    container.textContent = '';

    const grid = document.createElement('div');
    grid.className = 'tp-grid';
    container.appendChild(grid);

    let count = 0;
    for (const it of items) {
      if (!it || !it.Url) continue;
      grid.appendChild(createCell(it));
      count++;
    }
    if (!count) {
      container.textContent = 'No sounds available.';
      return;
    }

    // Event delegation → control the global player
    const { state, setIcon, stop } = getGlobalPlayer();

    grid.addEventListener('click', async (e) => {
      const btn = e.target.closest('.tp-btn');
      if (!btn || !grid.contains(btn)) return;

      const url = btn.dataset.url;
      if (!url) return;

      if (state.currentBtn === btn && !state.audio.paused) { // toggle to stop
        stop();
        return;
      }

      if (state.currentBtn && state.currentBtn !== btn) setIcon(state.currentBtn, false);

      state.currentBtn = btn;
      setIcon(btn, true);

      try {
        if (state.audio.src !== url) state.audio.src = url;
        await state.audio.play();
      } catch (err) {
        console.error('Audio play error:', err);
        setIcon(btn, false);
        if (state.currentBtn === btn) state.currentBtn = null;
      }
    });
  };

  /* ---------- Initialize containers ---------- */
  const initContainer = async (container) => {
    const pid = container.getAttribute('data-pid');
    if (!pid) { container.textContent = 'Missing data-pid attribute.'; return; }

    const apiOriginHint = container.getAttribute('data-api-origin') || null;

    const loading = document.createElement('div');
    loading.style.fontFamily = 'system-ui, Arial, sans-serif';
    loading.style.fontSize = '14px';
    loading.style.padding = '10px';
    loading.textContent = 'Loading sounds…';
    container.appendChild(loading);

    try {
      const sounds = await fetchSounds(pid, apiOriginHint);
      renderWidget(container, Array.isArray(sounds) ? sounds : []);
    } catch (err) {
      console.error('Failed to load sounds:', err);
      container.textContent = 'Failed to load sounds.';
    }
  };

  const init = () => {
    injectStyles();

    const legacy = document.getElementById('product-trigger-player');
    if (legacy) initContainer(legacy);

    const many = document.querySelectorAll('.product-trigger-player');
    many.forEach((c) => { if (c !== legacy) initContainer(c); });
  };

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', init, { once: true });
  } else {
    init();
  }
})();
