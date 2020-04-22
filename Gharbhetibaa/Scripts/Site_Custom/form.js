var $state = $('#ItemForID'), $property_type = $('#ItemTypeID');



$state.change(function () {
    //$property_type.attr('disabled', 'disabled').val('');

    if ($state.val() == '1') {
        var selectValues = { "1": "Room", "2": "Flat", "3": "Apartment", "4": "House", "5": "Land", "6": "Office Space", "7": "Shop Space" };
       // $property_type.removeAttr('disabled');
    } else if ($state.val() == '2') {
        var selectValues = { "1": "Room", "2": "Flat", "3": "Apartment", "4": "House", "5": "Land", "6": "Office Space", "7": "Shop Space", "8": "Commercial Property" };
       // $property_type.removeAttr('disabled');
    } else if ($state.val() == '3' || $state.val() == '4') {
        var selectValues = { "1": "Room", "2": "Flat", "3": "Apartment", "4": "House" };
       // $property_type.removeAttr('disabled');
    }

    $property_type.empty();
    
    $.each(selectValues, function (key, value) {
        $property_type
            .append($("<option></option>")
                .attr("value", key)
                .text(value));
        });
}).trigger('change'); // added trigger to calculate initial state

$property_type.change(function () {
    $('#lookingfor').show();
    $('#lifestyle').show();
    $('#roomdetails').show();

    //Hide for commercial prop and land
    if ($property_type.val() == '8' || $property_type.val() == '5') {
        $('#lookingfor').hide();
        $('#lifestyle').hide();
        $('#roomdetails').hide();
    }
}).trigger('change');