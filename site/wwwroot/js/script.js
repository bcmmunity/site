window.onload = function() {
  let lang_icon = document.getElementById('lang_icon').contentDocument;
  let lang_elem = lang_icon.getElementById("Capa_1");
  lang_elem.style.fill = "#434343";

  let join_icons = document.getElementsByClassName('join__img');
  let icons_arr = Array.from(join_icons);
  let icon_elem;
  icons_arr.forEach(function(elem) {
    icon_elem  = elem.contentDocument.getElementById("Capa_1");
    icon_elem.style.fill = "#434343";
  });

  let team_select = document.getElementsByClassName('team__selection')[0];
  if (team_select !== undefined) {
    team_select = team_select.children;
  }
  let kek = Array.from(team_select);
  kek.forEach(function(elem) {
    elem.addEventListener("click", function(event) {
      event.preventDefault();
    })
  });
}
