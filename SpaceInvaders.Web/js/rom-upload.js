import { methods } from './main.js'
import { readFileAsync } from './utilites.js'

const selectButton = document.querySelector('#romSelect');
const fileUpload = document.querySelector('#romUpload');

selectButton.addEventListener('click', () => fileUpload.click());
fileUpload.addEventListener('change', processRoms, false);

const uploadedRomFiles = {
    INVADERS_H: false,
    INVADERS_G: false,
    INVADERS_F: false,
    INVADERS_E: false,
};

/**
 * Reads ROM files selected in file input into unsigned
 * byte array buffer.
 * 
 * Returned buffer is then passed to WASM LoadRom method.
 * 
 * @param {Event} e - Fired HTMLElement change event
 */
async function processRoms(e) {
    const files = e.target.files;

    for (const file of files) {
        const name = file.name.replace('.', '_').toUpperCase();

        const romStatus = uploadedRomFiles[name];

        if (romStatus !== true && romStatus !== undefined) {
            await processRomFile(name, file);
        }

        updateList();
    }
}

async function processRomFile(name, file) {
    const data = new Uint8Array(await readFileAsync(file));

    methods.LoadRom(name, data);

    uploadedRomFiles[name] = true;
}

function updateList() {
    let romsUploaded = true;

    for (const [key, value] of Object.entries(uploadedRomFiles)) {
        if (value) {
            const icon = document.querySelector(`#${key}_Status`);

            icon.classList.remove("bi-exclamation-diamond-fill");
            icon.classList.add("bi-check-lg");
        }
        else {
            romsUploaded = false;
        }
    }

    if (romsUploaded) {
        selectButton.style.display = 'none';
        
        methods.StartGame();
    }
}