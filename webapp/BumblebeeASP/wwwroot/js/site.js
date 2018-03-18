// Write your JavaScript code.

//Scripts for when NewProjectView Partial is loaded 
$(function () {
    $('#startdatepicker').datetimepicker({
        useCurrent: true
    });
});

$(function() {
  $('input[name="CompanyOption"]').change(function() {
    if ($(this).val() == "customerOption1") {
      $("#existing-dropdown").show();
      $("#new-customer-form").hide();
    } else {
      $("#existing-dropdown").hide();
      $("#new-customer-form").show();
    }
  });
});

$(function() {
    $('.dropdown-menu option').click(function(){
        $('#selected').text($(this).text());
    });
});