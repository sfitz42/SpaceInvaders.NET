import { Methods } from './main.js'

const fileUpload = document.querySelector('#romSelect');
fileUpload.addEventListener('change', processRoms, false);

function readRomAsync(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onload = () => {
            resolve(reader.result);
        };

        reader.onerror = reject;

        reader.readAsArrayBuffer(file);
    })
}

async function processRoms(e) {
    const files = e.target.files;

    for (const file of files) {
        const name = file.name.replace('.', '_').toUpperCase();

        const data = new Uint8Array(await readRomAsync(file));

        console.log(name);
        console.log(data);

        Methods.LoadRom(name, data);
    }
}
