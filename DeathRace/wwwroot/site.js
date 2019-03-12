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
  
  // Retrieve Driver list and filtered car list
  $.when( $.ajax( "api/Driver" ), $.ajax( url ) ).done(function( a1, a2 ) {
      allDriverList = a1[ 0 ];
      filteredCarList = a2[ 0 ];
      var filteredDriverList = [];
      var driverIdList = [];
      
      // Get driverIdList from filteredCarList
      $.each( filteredCarList, function( index, car ){
        if(driverIdList.indexOf(car.driver.driverId) === -1) {
          driverIdList.push(car.driver.driverId);
        }
      });
      
      $.each( allDriverList, function( index, driver ){
        console.log(driver)
      });
  }
  
  $.get( "api/Driver", function( data ) {
    
    
    
    allDriverList = data;
    
    $.get( url, function( data ) {
      $('ul.driverlist').empty();
      var driverList = {};
      $.each( data, function( index, car ){
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


  });
}