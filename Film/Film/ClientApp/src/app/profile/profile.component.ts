import { Component, OnInit } from '@angular/core';
import { ExpressService } from '.././express.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
//import { Ng2TelInputModule } from 'ng2-tel-input';
declare var jquery: any;
declare var $: any;
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private accountService: ExpressService, private http: HttpClient) { }

  profileForm = new FormGroup({
    name: new FormControl(''),
    surname: new FormControl(''),
    state: new FormControl(''),
    city: new FormControl(''),
    address1: new FormControl(''),
    address2: new FormControl(''),
    postalCode: new FormControl(''),
    country: new FormControl(''),
    knowledges: new FormControl(''),
    phone: new FormControl('')
  });
  

  
  ngOnInit() {

    $(function () {
      var input = document.querySelector("#phone");
     
      var errorMsg = document.querySelector("#error-msg");
      var validMsg = document.querySelector("#valid-msg");

      // here, the index maps to the error code returned from getValidationError - see readme
      var errorMap = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];

      var iti =   (<any>window).intlTelInput(input, {
                initialCountry: "auto",
                utilsScript: "./utils.js",
                geoIpLookup: function (success, failure) {
                  $.ajax({
                    url: "https://ipinfo.io/json",
                    type: 'get',
                    processData: false,
                    beforeSend: function (xhr) {
                      xhr.setRequestHeader('HASH', '5c268592cd4db9c7f6b813bb689005c6');
                    },
                    success: function (resp) {
                      var countryCode = (resp && resp.country) ? resp.country : "";
                      success(countryCode);
                      console.log(countryCode);
                    },
                    error: function (xhr, status, error) {
                      console.log(xhr);
                    }
                  });
                },
              });

      var reset = function () {
       
        input.classList.remove("error");
        errorMsg.innerHTML = "";
        errorMsg.classList.add("hide");
        validMsg.classList.add("hide");
      };

      // on blur: validate
      input.addEventListener('blur', function () {
        reset();
        if ((<any>input).value.trim()) {
          if (iti.isValidNumber()) {
            validMsg.classList.remove("hide");
          } else {
            input.classList.add("error");
            var errorCode = iti.getValidationError();
            errorMsg.innerHTML = errorMap[errorCode];
            errorMsg.classList.remove("hide");
          }
        }
      });

      // on keyup / change flag: reset
      input.addEventListener('change', reset);
      input.addEventListener('keyup', reset);

    });
  }
  ngAfterViewInit() {

  }

  submit() {
    this.profileForm.get('knowledges').setValue((<any>document.getElementById("knowledges")).value);
    console.log(this.profileForm);
  }
}
