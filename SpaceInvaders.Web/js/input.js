import { methods } from './main.js'

const keyMap = {
    "LEFT": "Left",
    "RIGHT": "Right",
    " ": "Fire",
    "ENTER": "1UP",
    "BACKSPACE": "2UP",
    "C": "Credit"
};

function handleKey(event, pressed) {
    const key = event.key.replace("Arrow", "").toUpperCase();

    methods.SetKey(keyMap[key], pressed);

    event.preventDefault();
}

function registerButton(id, mappedAction) {
    const button = document.getElementById(id);

    button.addEventListener("mousedown", () => methods.SetKey(mappedAction, true));
    button.addEventListener("mouseup", () => methods.SetKey(mappedAction, false));
    button.addEventListener("touchstart", () => methods.SetKey(mappedAction, true));
    button.addEventListener("touchend", () => methods.SetKey(mappedAction, false));
}

window.addEventListener("keydown", (event) => handleKey(event, true));
window.addEventListener("keyup", (event) => handleKey(event, false));

registerButton('left', 'Left');
registerButton('right', 'Right');
registerButton('fire', 'Fire');
registerButton('1up', '1UP');
registerButton('2up', '2UP');
registerButton('credit', 'Credit');