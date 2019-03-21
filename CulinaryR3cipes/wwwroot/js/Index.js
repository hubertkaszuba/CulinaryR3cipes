$(function () {
    $("#typeDropDown").chosen({
        width: "33%"
    });
});

$(function () {
    $("#categoryIncludedDropDown").chosen({
        width: "33%"
    });
});

$(function () {
    $("#categoryExcludedDropDown").chosen();
    width: "33%"
});


$(document).ready(function () {
    var selectedTypeIds = $("#typeDropDown").val();
    $("#viewPlaceHolder").load("/Home/Recipes",
        { SelectedTypeIds: selectedTypeIds });
});

$("#typeDropDown").change(function (e) {
    var selectedTypeIds = $("#typeDropDown").val();
    $("#viewPlaceHolder").load("/Home/Recipes",
        { SelectedTypeIds: selectedTypeIds });
});

$("#btnSearch").on("click", function (e) {
    var selectedTypeIds = $("#typeDropDown").val();
    var includedCategoryIds = $("#categoryIncludedDropDown").val();
    var excludedCategoryIds = $("#categoryExcludedDropDown").val();
    $("#viewPlaceHolder").load("/Home/Recipes",
        { SelectedTypeIds: selectedTypeIds, IncludedCategoryIds: includedCategoryIds, ExcludedCategoryIds: excludedCategoryIds, recipePage: 1 });
});