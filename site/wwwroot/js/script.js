$(function() {
  $(".hamburger").on("click", function(e) {
    var l = $(this).attr("data-active");
    if (l === "false") {

      $(".overlay").css( "zIndex", 100 );
      $(".overlay").toggleClass("overlay__active");
      $(this).toggleClass("hamburger__active");
      $(this).attr("data-active", "true");
    } else {
      $(".overlay").css( "zIndex", -100 );
      $(".overlay").toggleClass("overlay__active");
      $(this).toggleClass("hamburger__active");
      $(this).attr("data-active", "false");
    }
  })
  $('.owl-carousel').owlCarousel({
    loop:true,
    items: 1,
    center: true,
    margin:20,
    nav:true,
})
$("#add").on("click", function(e) {
  e.preventDefault();
  var input = document.createElement("input");
  input.type = "text";
  input.name = "Name";
  input.required = true;
  console.log(input);
  $("#addForm .group").append(input);
});

})

$(function() {
  $("#addWork").on("click", function(e) {
    e.preventDefault();

    let cont = document.createElement("div");
    cont.classList.add("edit__courses__group");
    let group = document.createElement("div");
    group.classList.add("edit__group");

    let labelName = document.createElement("label")
    labelName.innerHTML = "Название";
    labelName.classList.add("second_label");
    group.append(labelName);
    let inputName = document.createElement("input");
    inputName.type = "text";
    inputName.name = "Titles";
    group.append(inputName)

    cont.append(group);

    group = document.createElement("div");
    group.classList.add("edit__group");



    let labelLink = document.createElement("label")
    labelLink.innerHTML = "Ссылка на сайт (если есть)";
    labelLink.classList.add("second_label");
    group.append(labelLink);
    let inputLink = document.createElement("input");
    inputLink.type = "text";
    inputLink.name = "Links";
    group.append(inputLink)

    cont.append(group);

    let labelDesc = document.createElement("label")
    labelDesc.innerHTML = "Краткое описание";
    labelDesc.classList.add("second_label");
    cont.append(labelDesc);

    let textarea = document.createElement("textarea");
    textarea.name = "Descriptions";
    cont.append(textarea);

    let labelStart = document.createElement("label")
    labelStart.innerHTML = "Дата начала";
    labelStart.classList.add("second_label");
    cont.append(labelStart)

    let inputDateStart = document.createElement("input");
    inputDateStart.type = "date";
    inputDateStart.name = "StartDates";

    cont.append(inputDateStart);

    let labelFinish = document.createElement("label")
    labelFinish.innerHTML = "Дата конца";
    labelFinish.classList.add("second_label");
    cont.append(labelFinish)

    let inputDateFinish = document.createElement("input");
    inputDateFinish.type = "date";
    inputDateFinish.name = "FinishDates";

    cont.append(inputDateFinish);

    let labelWork = document.createElement("label")
    labelWork.innerHTML = "Работа";
    labelWork.classList.add("second_label");
    cont.append(labelWork)

    let inputWork = document.createElement("input");
    inputWork.type = "checkbox";
    inputWork.name = "IsWorks";
    cont.append(inputWork)


    console.log(cont);
    $("#work").append(cont);
  });
})
