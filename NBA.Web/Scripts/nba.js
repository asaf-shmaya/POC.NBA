var app = [];
app.data = [];

app.consts = [];
app.consts.url = [];
app.consts.url.GetPlayersProfilesTeamsConfigAll = "https://localhost:44388/api/Players/Profiles/Teams/Config/All?year={0}";

app.functions = [];
app.functions.GetData = GetData;

function GetData(year) {
    $.ajax({
        method: 'GET',
        url: app.consts.url.GetPlayersProfilesTeamsConfigAll.format(year),
        data: {},
        beforeSend: function (xhr) {
            // Load loader
        },
        success: function (result, status, xhr) {
            app.data = result;
            // Populate results
        },
        error: function (xhr, status, error) {
            console.log('Error statusText: ' + xhr.statusText);
        },
        complete: function (xhr, status) {
            // Set unload loader.
        }
    });
}

// Passing a named function instead of an anonymous function.
function readyFn(jQuery) {
    // Code to run when the document is ready.
    $("#divBtnApply").click(() => { alert("#divBtnApply" + " click!") });
    
}


$(document).ready(readyFn);