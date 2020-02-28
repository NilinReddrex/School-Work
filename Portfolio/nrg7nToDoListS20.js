function submitForm() {
    var formTitle = document.getElementById("formTitle");
    if (formTitle.value == "") {
        alert("Title must be filled out");
        return;
    }
    var formType = document.getElementById("formType");
    if (formType.selectedIndex == 0) {
        alert("Type must be filled out");
        return;
    }
    var formDate = document.getElementById("formDate");
    if (formDate.value == "") {
        alert("Date must be filled out");
        return;
    }

    var new_row = document.createElement('div');
    new_row.className = "row";

    var titleCol = document.createElement('div');
    titleCol.className = "itemHeads";
    titleCol.textContent = formTitle.value;

    new_row.appendChild(titleCol);
    $(".itemsContainer").append(new_row);

    var typeCol = document.createElement('div');
    typeCol.className = "itemHeads";
    typeCol.textContent = formType.value;

    new_row.appendChild(typeCol);
    $(".itemsContainer").append(new_row);

    var dateCol = document.createElement('div');
    dateCol.className = "itemHeads";
    dateCol.textContent = formDate.value;

    new_row.appendChild(dateCol);
    $(".itemsContainer").append(new_row);

    var actionCol = document.createElement('div');
    actionCol.className = "itemHeads";
    actionCol.textContent = "glyphicon glyphicon-ok";

    new_row.appendChild(actionCol);
    $(".itemsContainer").append(new_row);

    /*$(".itemsContainer").append(
        " <div class='row'>" +
        " <div class='itemHeads'>" + formTitle.value + "</div>" +
        " <div class='itemHeads'>" + formType.value + "</div>" +
        " <div class='itemHeads'>" + formDate.value + "</div>" +
        " <div class='itemHeads' onclick='rowDelete(this)'>" + " <span class='	glyphicon glyphicon-ok'></span>" + "</div>" +
        "</div>");*/

    clearForm();
}

function clearForm() {
    var textareaObject = document.getElementById("formTitle");
    textareaObject.value = null;
    var textareaObject = document.getElementById("formDate");
    textareaObject.value = null;
    var textareaObject = document.getElementById("formType")
    textareaObject.selectedIndex = 0;
}

function rowDelete(clickedObject) {
    clickedObject.parentElement.remove();
}
/* Reference: https://www.w3schools.com/howto/howto_js_toggle_dark_mode.asp*/
function darkLightTheme() {
    var element = document.body;
    element.classList.toggle("dark-mode");


    var elem = document.getElementById("currentTheme");
    if (elem.innerHTML == "Light Theme") {
        elem.innerHTML = "Dark Theme";
    } else if (elem.innerHTML == "Dark Theme") {
        elem.innerHTML = "Light Theme";
    }
}
/* Reference: https://www.w3schools.com/jsref/met_win_prompt.asp*/
function editTitle() {
    var editedTitle = prompt("Change title.");
    if (editedTitle !== null) {
        document.getElementById("title").innerHTML = editedTitle;
    } else if (editedTitle == null) {
        document.getElementById("title").innerHTML = "To Do List";
    }

}
/*end*/
