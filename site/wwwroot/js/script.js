
window.onload = function() {

    // var lang_icon = document.getElementById('lang_icon').contentDocument;
    // var lang_elem = lang_icon.getElementById("Capa_1");
    // lang_elem.style.fill = "#434343";

    var join_icons = document.getElementsByClassName('join__img');
    var icons_arr = Array.from(join_icons);
    var icon_elem;
    icons_arr.forEach(function(elem) {
        icon_elem  = elem.contentDocument.getElementById("Capa_1");
        icon_elem.style.fill = "#434343";
    });



};

$(function() {
  $(".hamburger").on("click", function(e) {
    var l = $(this).attr("data-active");
    if (l === "false") {
      $(".overlay").toggleClass("overlay__active");
      $(this).toggleClass("hamburger__active");
      $(this).attr("data-active", "true");
    } else {
      $(".overlay").toggleClass("overlay__active");
      $(this).toggleClass("hamburger__active");
      $(this).attr("data-active", "false");
    }
  })
})
