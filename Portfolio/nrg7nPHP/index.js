//References: hide() method and show() method https://www.w3schools.com/jquery/jquery_hide_show.asp 
function showForm(element){
    $(".formField").hide();
    if(element !== undefined){
        $("#" + element.value).show();
    }
}
  /* References:
   Switch statement https://www.w3schools.com/js/js_switch.asp
   isNumeric() https://api.jquery.com/jQuery.isNumeric/
   parseInt() https://www.w3schools.com/jsref/jsref_parseint.asp
   preventDefault() https://www.w3schools.com/jsref/event_preventdefault.asp
    */
function submitForm(e){
    var formElementId = e.target.id;
    switch(formElementId) {
        case "greetingFunction":
          var fname = $("#" + formElementId + "> #fname")[0].value;
          var lname = $("#" + formElementId + "> #lname")[0].value;
          break;

        case "factorialFunction":
          var number = $("#" + formElementId + "> #number")[0].value;
          if($.isNumeric(number) === false || parseInt(number) <= 0){
            alert("A positive integer is required.");
            e.preventDefault();
          }
          break;

        case "stringFunction":
          var string = $("#" + formElementId + "> #string")[0].value;
          break;

        case "sumSquaresFunction":
          var n1 = $("#" + formElementId + "> #n1")[0].value;
          var n2 = $("#" + formElementId + "> #n2")[0].value;
          if($.isNumeric(n1) === false || $.isNumeric(n2) === false){
            alert("An integer is required.");
            e.preventDefault();
          }

          if(parseInt(n1) < 1)
          {
            alert("n1 must be 1 or greater.");
            e.preventDefault();
          }
          else if(parseInt(n1) >= parseInt(n2)){
            alert("n1 must be less than n2.");
            e.preventDefault();
          }
          break;
        default:
      }
}

function clearForm(formElementId){
    var fields = $("#" + formElementId + "> .field");
        
    for(i = 0; i < fields.length; i++){
        $("#" + formElementId + "> #" + fields[i].name).val("");
    }
}

