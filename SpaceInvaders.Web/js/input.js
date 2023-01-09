import { methods } from './main.js'

window.addEventListener("keydown", (event) => methods.SetKey(event.key.replace("Arrow", ""), true));
window.addEventListener("keyup", (event) => methods.SetKey(event.key.replace("Arrow", ""), false));