(function () {
    const container = document.getElementById('product-audio-player');
    if (!container) return;

    const pid = container.dataset.pid;
    const prefix = container.dataset.prefix;
    const start = parseInt(container.dataset.start, 10);
    const end = parseInt(container.dataset.end, 10);

    // Inject CSS dynamically into body
    const style = document.createElement('style');
    style.textContent = `
        .player-container {
            display: flex;
            align-items: center;
            gap: 1em;
            font-family: sans-serif;
        }

        .player-container #triggerDisplay {
            font-weight: bold;
            min-width: 150px;
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

    // Build DOM
    const display = document.createElement('div');
    display.id = 'triggerDisplay';
    display.textContent = 'Current: (not started)';

    const button = document.createElement('button');
    button.textContent = 'Listen';

    const player = document.createElement('audio');
    player.id = 'audioPlayer';
    player.controls = true;

    const wrapper = document.createElement('div');
    wrapper.className = 'player-container';
    wrapper.appendChild(display);
    wrapper.appendChild(button);
    wrapper.appendChild(player);
    container.appendChild(wrapper);

    // Preload audio
    const triggerList = [];
    const audioCache = [];
    for (let i = start; i <= end; i++) {
        const trigger = `${prefix}${i}`;
        triggerList.push(trigger);
        const url = `https://userimages-akm.imvu.com/productdata/${pid}/1/${trigger}.ogg`;
        const audio = new Audio(url);
        audio.preload = 'auto';
        audioCache.push(audio);
    }

    let currentIndex = 0;

    function playNext() {
        if (currentIndex < audioCache.length) {
            const audio = audioCache[currentIndex];
            display.textContent = `Current: ${triggerList[currentIndex]}`;
            audio.play().catch(console.error);
            audio.onended = playNext;
            currentIndex++;
        } else {
            display.textContent = 'Playlist finished.';
        }
    }

    button.addEventListener('click', () => {
        currentIndex = 0;
        playNext();
    });
})();
