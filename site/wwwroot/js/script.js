var api;

function readURL(input) {

  if (input.files && input.files[0]) {
    var reader = new FileReader();

    reader.onload = function(e) {

      $('#blah').attr('src', e.target.result).Jcrop(
          {
            aspectRatio: 1,
            bgColor: "#000000",
            bgOpacity: .3,
            setSelect: [75, 75, 300, 300],
            onSelect: updateCoords
          }
      );

    };

    reader.readAsDataURL(input.files[0]);
  }
}

function updateCoords(c) {
  $("#cropX").val(c.x);
  $("#cropY").val(c.y);
  $("#cropWidth").val(c.w);
  $("#cropHeight").val(c.h);
}
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
  let input = document.createElement("input");
  input.type = "text";
  input.name = "Names";
  input.required = true;
  console.log(input);
  $("#addForm .group").append(input);
});

$("#addProject").on("click", function(e) {
  e.preventDefault();
  let input = document.createElement("input");
  input.type = "text";
  input.name = "Links";
  $("#addProjectContainer").append(input);
});




})

$(function() {
  $("#select").selectric();
  $("#selectUsers").selectric();
  $("#selectLeader").selectric();


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
    inputName.id = "Titles";
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
    inputLink.id = "Links";
    group.append(inputLink)

    cont.append(group);

    let labelDesc = document.createElement("label")
    labelDesc.innerHTML = "Краткое описание";
    labelDesc.classList.add("second_label");
    cont.append(labelDesc);

    let textarea = document.createElement("textarea");
    textarea.name = "Descriptions";
    textarea.id = "Descriptions";
    cont.append(textarea);

    let labelStart = document.createElement("label")
    labelStart.innerHTML = "Дата начала";
    labelStart.classList.add("second_label");
    cont.append(labelStart)

    let inputDateStart = document.createElement("input");
    inputDateStart.type = "date";
    inputDateStart.name = "StartDates";
    inputDateStart.id = "StartDates";

    cont.append(inputDateStart);

    let labelFinish = document.createElement("label")
    labelFinish.innerHTML = "Дата конца";
    labelFinish.classList.add("second_label");
    cont.append(labelFinish)

    let inputDateFinish = document.createElement("input");
    inputDateFinish.type = "date";
    inputDateFinish.name = "FinishDates";
    inputDateFinish.id = "FinishDates";

    cont.append(inputDateFinish);

    let labelWork = document.createElement("label")
    labelWork.innerHTML = "Работа";
    labelWork.classList.add("second_label");
    cont.append(labelWork)

    let inputWork = document.createElement("input");
    inputWork.type = "checkbox";
    inputWork.name = "IsWorks";
    inputWork.id = "IsWorks";
    inputWork.value = "true";
    cont.append(inputWork);
      let labelNow = document.createElement("label");
      labelNow.innerHTML = "По настоящее время";
      labelNow.classList.add("second_label");
      cont.append(labelNow);

      let inputNow = document.createElement("input");
      inputNow.type = "checkbox";
      inputNow.name = "Nows";
      inputNow.id = "Now";
      inputNow.value = "true";
      cont.append(inputNow);

    console.log(cont);
    $("#work").append(cont);
  });
  $("#fileUploader").change(function() {
    readURL(this);
  });
})
