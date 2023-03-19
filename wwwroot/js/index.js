$(document).ready(function () {
    var x = 0;
    var s = "";

    console.log("Hello Pluralsight");



    /*var theForm = document.getElementById("theForm");
    theForm.hidden = true;*/
    var theForm = $("#theForm");
    theForm.hide();

    /*var button = document.getElementById("#buyButton");
    button.addEventListener("click", function () {
        alert("Buying Item")
    });*/
    var button = $("#buyButton");
    button.on("click", function () {
        console.log("Buying Item");
    });


    /*var productInfo = document.getElementsByClassName("product-props");
    var listItems = productInfo.item[0].children;*/
    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.fadeToggle(1000);
    });

});

