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
})
