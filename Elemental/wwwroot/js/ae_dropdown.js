﻿window.aedropdown = {
    dropdowns: [],
    initialize: (id) => {
        window.aedropdown.dropdowns.push(id);
    },
    toggleVisibility: (id) => {
        let element = document.querySelector(`[ae-id="${id}"]`);
        element.classList.toggle("visible");
    }
}

document.addEventListener('click', function (event) {
    window.aedropdown.dropdowns.forEach(id => {
        let element = document.querySelector(`[ae-id="${id}"]`);
        if (element.classList.contains("visible") && !element.contains(event.target)) {
            element.classList.remove("visible");
        }
    })
})
