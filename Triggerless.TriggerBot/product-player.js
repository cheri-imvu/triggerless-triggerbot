(function () {
    const container = document.getElementById('product-audio-player');
    if (!container) return;

    const pid = container.dataset.pid;
    const prefix = container.dataset.prefix;
    const start = parseInt(container.dataset.start);
    const end = parseInt(container.dataset.end);

    // Inject CSS once per page
    if (!document.getElementById('product-player-style')) {
        const style = document.createElement('style');
        style.id = 'product-player-style';
        style.textContent = `
            .player-container {
                display: flex;
                align-items: center;
                gap: 1em;
                font-family: sans-serif;
                margin: 1em 0;
            }
            .player-container #triggerDisplay {
                font-weight: bold;
                min-width: 160px;
            }
            .player-container button {
                padding: 6px 12px;
                font-size: 14px;
            }
            .player-container audio {
                width: 300px;
            }
        `;
        document.body.appendChild(style);
    }

    // Build DOM
    const wrapper = document.createElement('div');
    wrapper.className = 'player-container';

    const display = document.createElement('div');
    display.id = 'triggerDisplay';
    display.textContent = 'Current: (not started)';
    wrapper.appendChild(display);

    const button = document.createElement('button');
    button.textContent = 'Listen';
    wrapper.appendChild(button);

    const audio = document.createElement('audio');
    audio.controls = true;
    wrapper.appendChild(audio);

    container.appendChild(wrapper);

    // Build trigger list with display and padded file name
    const triggers = [];
    for (let i = start; i <= end; i++) {
        const padded = i.toString().padStart(3, '0');
        const fileName = `${prefix}${padded}`;
        const displayName = `${prefix}${i}`;
        const url = `https://userimages-akm.imvu.com/productdata/${pid}/1/${fileName}.ogg`;
        triggers.push({ displayName, url });
    }

    const cache = [];
    let currentIndex = 0;

    async function preloadAudio() {
        display.textContent = 'Preloading audio...';

        for (let i = 0; i < triggers.length; i++) {
            const { displayName, url } = triggers[i];
            try {
                const res = await fetch(url);
                if (!res.ok) throw new Error(`HTTP ${res.status}`);
                const blob = await res.blob();
                const objectUrl = URL.createObjectURL(blob);
                cache[i] = { displayName, objectUrl };
                console.log(`Cached: ${displayName}`);
            } catch (err) {
                console.warn(`Failed to preload ${displayName}:`, err);
                cache[i] = null;
            }
        }

        display.textContent = 'Ready. Click Listen to begin.';
    }

    function playFromCache(index) {
        if (index >= cache.length) {
            display.textContent = 'Playlist finished.';
            return;
        }

        const entry = cache[index];
        if (!entry) {
            display.textContent = `Skipping missing: ${triggers[index].displayName}`;
            playFromCache(index + 1);
            return;
        }

        display.textContent = `Playing: ${entry.displayName}`;
        audio.src = entry.objectUrl;
        audio.load();

        const tryPlay = () => {
            audio.play().catch(err => {
                console.warn(`Playback failed for ${entry.displayName}:`, err);
                display.textContent = `Error: ${entry.displayName}`;
                playFromCache(index + 1);
            });
        };

        // Wait until it's ready before trying to play
        const onCanPlay = () => {
            audio.removeEventListener('canplaythrough', onCanPlay);
            tryPlay();
        };

        audio.addEventListener('canplaythrough', onCanPlay);
        audio.onended = () => playFromCache(index + 1);
    }

    button.addEventListener('click', () => {
        currentIndex = 0;
        playFromCache(currentIndex);
    });

    preloadAudio();
})();
