// Passing a named function instead of an anonymous function.
function readyFn(jQuery) {
    // Code to run when the document is ready.
    $("#divBtnApply").click(() => { alert("#divBtnApply" + " click!") });
}

$(document).ready(readyFn);

