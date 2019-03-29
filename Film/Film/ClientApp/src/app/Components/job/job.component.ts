import { Component, OnInit } from '@angular/core';
import { KnowledgesComponent } from '../knowledges/knowledges.component';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { ExpressService } from '../.././express.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {
  public jobForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private expressService: ExpressService, private http: HttpClient) {
    this.jobForm = this.formBuilder.group({
      tittle: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      jobDescription: new FormControl('', [Validators.required, Validators.maxLength(300)]),
      knowledges: new FormControl('', Validators.required),
      explanation: new FormControl(''),
      guidId: new FormControl(''),
      jobImages: this.formBuilder.array([]),
      jobKnowledges: this.formBuilder.array([])
    });
  }


  ngOnInit() {
  }

  // We will create multiple form controls inside defined form controls photos.
  createItem(data): FormGroup {
    return this.formBuilder.group(data);
  }

  //Help to get all photos controls as form array.
  get jobImages(): FormArray {
    return this.jobForm.get('jobImages') as FormArray;
  };

  get jobKnowledges(): FormArray {
    return this.jobForm.get('jobKnowledges') as FormArray;
  };

  detectFiles(event) {
    let files = event.target.files;
    if (files) {
      for (let file of files) {
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.jobImages.push(this.createItem({
            Img: e.target.result  //Base64 string for preview image
          }));
        }
        reader.readAsDataURL(file);
      }
      console.log(this.jobForm.get('jobImages'));
    }
  }



  submit() {
    this.jobForm.get('knowledges').setValue((<any>document.getElementById("knowledges")).value);
    this.jobForm.get('explanation').setValue((<any>document.getElementById("explanation")).value);
    console.log(this.jobForm);

    var knowledges = this.jobForm.value.knowledges.split(" - ");
    var explanations = this.jobForm.value.explanation.split(" - ");
    var knowledgeObject = [knowledges.length];

    for (var i = 0; i < knowledges.length; i++) {
      knowledgeObject[i] = new Object();
      (<any>knowledgeObject)[i].Value = knowledges[i];
      (<any>knowledgeObject)[i].Explanation = explanations[i];
      //this.jobKnowledges.push(this.createItem({
      //  Value: knowledges[i],
      //  Explanation: explanations[i]
      //}));
    }



    this.expressService.setJob(this.jobForm.value).subscribe(response => {
      

      console.log(response);

      this.expressService.setKnowledgesJob(knowledgeObject).subscribe(response => {
        console.log(response);
      })

    });

  }

}
