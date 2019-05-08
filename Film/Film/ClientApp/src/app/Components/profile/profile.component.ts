import { Component, OnInit } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { GeneralService } from '../.././general.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { error } from 'util';
import { ok } from 'assert';

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
  cCode: string;

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
    
    //this.profileForm.setValue(this.accountService.user.userDates);
    this.profileForm.get('name').setValue(this.accountService.user.userDates.name);
    this.profileForm.get('surname').setValue(this.accountService.user.userDates.surname);
    this.profileForm.get('state').setValue(this.accountService.user.userDates.state);
    this.profileForm.get('city').setValue(this.accountService.user.userDates.city);
    this.profileForm.get('address1').setValue(this.accountService.user.userDates.address1);
    this.profileForm.get('address2').setValue(this.accountService.user.userDates.address2);
    this.profileForm.get('postalCode').setValue(this.accountService.user.userDates.postalCode);
    this.profileForm.get('country').setValue(this.accountService.user.userDates.country);
    this.profileForm.get('phone').setValue(this.accountService.user.userDates.phone);
    this.profileForm.get('personalInfo').setValue(this.accountService.user.userDates.personalInfo);
    this.profileForm.get('profileImg').setValue(this.accountService.user.userDates.profileImg);
    (<HTMLImageElement>document.getElementById("prueba")).src = this.accountService.user.userDates.profileImg;

    //this.accountService.getProfile().subscribe(response => {    
    //  console.log(response);
    //  if (response != null) {      
    //    response.knowledges = "";
    //    response.explanation = "";
    //    response.guidId = "";
    //    delete response.$id;
    //    delete response.id;
    //    delete response.score;
    //    delete response.user;
    //    delete response.profileImgString;
    //    this.profileForm.setValue(response);
    //  }
    //  console.log(this.profileForm);
    //});


   
   //this.cCode = countryCode.toLowerCase();

    const req = this.http.get<any>('https://api.ipdata.co/?api-key=test');
    // 0 requests made - .subscribe() not called.
    req.subscribe((response)=>{
      (<any>document).getElementById("city").value = response.city;
      (<any>document).getElementById("state").value = response.region;
      (<any>document).getElementById("zip").value = response.postal;
      this.cCode = response.country_code.toLowerCase();
        console.log(this.cCode);    
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
