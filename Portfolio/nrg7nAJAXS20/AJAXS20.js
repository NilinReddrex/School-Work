/* References for JS file:
https://missouri.instructure.com/courses/28042/pages/lecture-20-ajax?module_item_id=1756881
https://www.youtube.com/watch?list=PLpvL1C_oZsr85ecxCutCn64vR2NyVEm6G&time_continue=522&v=BKsJlSWMxOc&feature=emb_logo
https://missouri.instructure.com/courses/28042/pages/lecture-21-web-services?module_item_id=1756883
https://www.youtube.com/watch?list=PLpvL1C_oZsr9CxesLFJZxe1f435rwBtPl&time_continue=342&v=Mow-TD0XLnE&feature=emb_logo
*/

function init() {
    getInfo();
    getClassType();
    getCurrentAssignment();
}

function getInfo() {
    $("#classInfo").html(getLoader());
    var options;

    options = {
        "content": "info"
    };

    var callBack = function (data) {
        $("#classInfo").html(data);

    };
    getContent(options, callBack);
}

function getClassType() {
    $("#classType").html(getLoader());

    var options;

    options = {
        "content": "classType"
    };

    var callBack = function (data) {
        $("#classType").html(data);

    };
    getContent(options, callBack);
}

function getCurrentAssignment() {
    $("#currentAssign").html(getLoader());
    var options;

    options = {
        "content": "currentAssignment",
        "format": "json"
    };
    // Timestanp Reference: https://www.geeksforgeeks.org/how-to-convert-unix-timestamp-to-time-in-javascript/
    var callBack = function (data) {
        var d = new Date(parseInt(data.dueDateTimestamp) * 1000);
        var output = data.assignmentName + " <br/> " + d.toString();
        $("#currentAssign").html(output);

    };
    getContent(options, callBack);
}

function getOfficeHours(element, day) {
    $("#officeHours").html(getLoader());

    $(".tablinks").removeClass("active");
    $(element).addClass("active");
    var xmlHttp = new XMLHttpRequest();

    xmlHttp.onload = function () {
        if (xmlHttp.status == 200) {
            
            var xmlDoc = xmlHttp.responseXML;
            var output = '<ul>';

            var person = xmlDoc.getElementsByTagName("person");
            var time = xmlDoc.getElementsByTagName("time");
            var location = xmlDoc.getElementsByTagName("location");

            for (var i = 0; i < person.length; i++) {
                // if == null continue;
                output += "<li>" + person[i].childNodes[0].nodeValue + " has office hours from " + time[i].childNodes[0].nodeValue + " at " + location[i].childNodes[0].nodeValue + "</li>";
            }

            output += "</ul>";
            var divObj = document.getElementById('officeHours');
            divObj.innerHTML = output;
        }
    };

    xmlHttp.open("GET", "https://www.mizzouit.com/2830/challenge9/classInfo.php?content=officeHours&format=xml&day=" + day, true);
    xmlHttp.send();

}

function getContent(options, callBack) {
    $.get("https://www.mizzouit.com/2830/challenge9/classInfo.php", options, callBack);

}

function getLoader() {
    var loader = document.createElement("img");
    loader.src = "https://media.giphy.com/media/L05HgB2h6qICDs5Sms/source.gif";
    loader.height = 48;
    loader.width = 48;
    return loader;
}