$( document ).ready(function() {
    updateOwners();
});

$(document).on('input', 'input:text', function() {
  updateOwners(parseInt($(this).val()));
});



function updateOwners(year = null) {
  
  if (year > 0) {
    var url = "api/Car?startyear=" + year;
  }else{
    var url = "api/Car";
  }
  
  $.get( url, function( data ) {
    $('ul.driverlist').empty();
    var driverList = {};
    $.each( data, function( index, car ){
      carList = car.driver.cars;
      carList.push({
        carId: car.carId,
        brand: car.brand,
        model: car.model,
        type: car.type,
        year: car.year
      })
      car.driver.cars = carList;
      car.driver.preposition = car.driver.preposition ? car.driver.preposition : '';
      driverList[car.driver.driverId] = car.driver;
    });

    $.each( driverList, function( id, driver ){
      var cars = $('<ul>');
      $.each( driver.cars, function( id, car ){
          cars.append(
            $('<li>').text(car.brand + ', ' + car.model + ', ' + car.type + ', ' + car.year)
          )
      });
      $('ul.driverlist').append(
        $('<li>').text(driver.givenName + ' ' + driver.preposition + ' ' + driver.lastName)
          .append( cars)
      );
    });


  });
}