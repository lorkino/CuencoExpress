import { Component, OnInit } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { GeneralService } from '../.././general.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { error } from 'util';
//import { Ng2TelInputModule } from 'ng2-tel-input';
declare var jquery: any;
declare var $: any;
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private accountService: ExpressService, private http: HttpClient, private generalService: GeneralService) { }
  imageUrl: any;


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
    explanation: new FormControl(''),
    guidId: new FormControl(''),
    phone: new FormControl(''),
    personalInfo: new FormControl(''),
    profileImg: new FormControl([])
  });
  

  
  ngOnInit() {


    this.accountService.getProfile(this.profileForm).subscribe(response => {
      
      console.log(response);
      if (response != null) {
        if (response.profileImgString) {
          (<any>document).getElementById("prueba").src = "data:image/jpeg;base64," + response.profileImgString;
          //Para enviar el objeto response directamente necesitamos añadir y borrar propiedades
          response.profileImg = response.profileImgString;
        }
        response.knowledges = "";
        response.explanation = "";
        response.guidId = "";
        delete response.$id;
        delete response.id;
        delete response.score;
        delete response.user;
        delete response.profileImgString;
        this.profileForm.setValue(response);
      }
      console.log(this.profileForm);
    });
    

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
                    url: "https://api.ipdata.co/?api-key=test",
                    type: 'get',
                    processData: false,
                    beforeSend: function (xhr) {
                     
                    },
                    success: function (resp) {
                      var countryCode = (resp && resp.country_code) ? resp.country_code : "";
                      success(countryCode);                  
                     (<any>document).getElementById("city").value = resp.city;
                     (<any>document).getElementById("state").value = resp.region;
                     (<any>document).getElementById("zip").value = resp.postal;                     
                    },
                    error: function (xhr, status, error) {
                      console.log(xhr);
                    }
                  });
                },
              });
      console.log(iti);
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


  imageUpload(event: any) {
  const reader = new FileReader();
        if(event.target.files && event.target.files.length) {
        const [file] = event.target.files;
        reader.readAsDataURL(file);
          reader.onloadend = () => {
          (<any>document).getElementById("prueba").src = reader.result;
          this.profileForm.patchValue({
            profileImg: (<any>reader.result).split(",")[1]
          });

        };
      }
  }
 

  submit() {
    this.profileForm.get('knowledges').setValue((<any>document.getElementById("knowledges")).value);
    this.profileForm.get('explanation').setValue((<any>document.getElementById("explanation")).value);
    console.log(this.profileForm);
    this.accountService.changeProfile(this.profileForm.value).subscribe(response => {
      var knowledges = this.profileForm.value.knowledges.split(" - ");
      var explanations = this.profileForm.value.explanation.split(" - ");
      var knowledgeObject = [knowledges.length];

      for (var i = 0; i < knowledges.length; i++)
      {
        knowledgeObject[i] = new Object();
        (<any>knowledgeObject)[i].Value = knowledges[i];
        (<any>knowledgeObject)[i].Explanation = explanations[i];
      }
   

      this.accountService.setKnowledges(knowledgeObject).subscribe(response => {
        console.log(response);
      });
    });
    
  }
}
