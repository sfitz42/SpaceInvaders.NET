import { methods } from './main.js'

window.addEventListener("keydown", (event) => {
    methods.SetKey(event.key.replace("Arrow", ""), true);

    event.preventDefault();
});

window.addEventListener("keyup", (event) => {
    methods.SetKey(event.key.replace("Arrow", ""), false);

    event.preventDefault();
});