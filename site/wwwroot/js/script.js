
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
