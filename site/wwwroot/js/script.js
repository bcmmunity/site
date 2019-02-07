function fillSVG() {
    var not_filled = document.querySelectorAll("object[data-filled=false]");
    not_filled = Array.from(not_filled);
    not_filled.forEach(function(elem){
        elem.setAttribute("data-filled", "true");
        icon_elem  = elem.contentDocument.getElementById("Capa_1");
        icon_elem.style.fill = "#434343";
    });
}

$(function() {
    var project_count = 4;
    var team_count = 4;

    $(this).on("click", "#projects__load__button", function() {
        console.log(project_count);
        project_count += 4;
        $.ajax({
            type: "GET",
            url: "/Home/ProjectsLoad",
            data: { offset: project_count, count: 4 },
            success: function (result) {
                $("#pr").append(result);
            }
        });

    });

    $(this).on("click", "#team__load__button", function() {
        console.log(team_count);
        team_count += 6;
        $.ajax({
            type: "GET",
            url: "/Home/TeamsLoad",
            data: { offset: team_count, count: 6},
            success: function (result) {
                $("#teams").append(result);
                fillSVG();
            }
        });

    });

});

window.onload = function() {

    var lang_icon = document.getElementById('lang_icon').contentDocument;
    var lang_elem = lang_icon.getElementById("Capa_1");
    lang_elem.style.fill = "#434343";

    var join_icons = document.getElementsByClassName('join__img');
    var icons_arr = Array.from(join_icons);
    var icon_elem;
    icons_arr.forEach(function(elem) {
        icon_elem  = elem.contentDocument.getElementById("Capa_1");
        icon_elem.style.fill = "#434343";
    });

    fillSVG();

};
