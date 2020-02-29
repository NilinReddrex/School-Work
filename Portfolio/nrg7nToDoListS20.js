/* References: 
https://missouri.instructure.com/courses/28042/pages/lecture-08-html-forms?module_item_id=1756955
https://missouri.instructure.com/courses/28042/pages/lecture-09-javascript-and-dom?module_item_id=1756957
https://missouri.instructure.com/courses/28042/pages/lecture-10-accessing-form-data?module_item_id=1756942
https://missouri.instructure.com/courses/28042/pages/lecture-11-jquery?module_item_id=1756944
*/

function submitForm() {
    // Clear validation error on submit
    $("#errorMessage").html("");

    // Get current form values
    var formTitle = document.getElementById("formTitle");
    var formType = document.getElementById("formType");
    var formDate = document.getElementById("formDate");

    // Validate form values are correct (set error message if invalid)
    if (formTitle.value == "" || formType.selectedIndex == 0 || formDate.value == "") {
        $("#errorMessage").html("All fields must be filled out!");
        return;
    }
    //Reference: https://www.w3schools.com/jsref/met_document_createelement.asp
    // Create a new to-do row.
    var new_row = document.createElement('div');
    new_row.className = "row";

    // Create the title column and add it to the row.
    var titleCol = document.createElement('div');
    titleCol.className = "itemHeads";
    titleCol.textContent = formTitle.value;
    new_row.appendChild(titleCol);

    // Create the type column and add it to the row.
    var typeCol = document.createElement('div');
    typeCol.className = "itemHeads";
    typeCol.textContent = formType.value;
    new_row.appendChild(typeCol);

    // Create the date column and add it to the row.
    var dateCol = document.createElement('div');
    dateCol.className = "itemHeads";
    dateCol.textContent = formDate.value;
    new_row.appendChild(dateCol);

    // Create the action column, and the checkmark within it, and add it to the row.
    var actionCol = document.createElement('div');
    actionCol.className = "itemHeads";
    var checkMark = document.createElement('span');
    checkMark.className = "glyphicon glyphicon-ok";

    // Add an onclick event that will remove the row when clicked.
    checkMark.addEventListener("click", function () {
        rowDelete(new_row);
    });

    actionCol.appendChild(checkMark);
    new_row.appendChild(actionCol);

    // Add the new row to the items container.
    $(".itemsContainer").append(new_row);

    // Clear the form after submission.
    // end of reference
    clearForm();
}
//Reference: https://www.youtube.com/watch?list=PLpvL1C_oZsr-ip4CO7vR98o8GB-cqdvif&time_continue=9&v=Qpd-Gfa4qYk&feature=emb_logo Lecture Material
function clearForm() {
    var textareaObject = document.getElementById("formTitle");
    textareaObject.value = null;
    var textareaObject = document.getElementById("formDate");
    textareaObject.value = null;
    var textareaObject = document.getElementById("formType")
    textareaObject.selectedIndex = 0;
}
//Reference: https://www.w3schools.com/jsref/met_select_remove.asp
function rowDelete(clickedObject) {
    clickedObject.remove();
}
/* Reference: https://www.w3schools.com/howto/howto_js_toggle_dark_mode.asp*/
function darkLightTheme() {
    $(".themed").toggleClass("dark-mode");

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
    if (editedTitle !== null && editedTitle !== "") {
        document.getElementById("title").innerHTML = editedTitle;
    }
}
