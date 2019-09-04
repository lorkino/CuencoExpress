import { Component, OnInit } from '@angular/core';
import { KnowledgesComponent } from '../knowledges/knowledges.component';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { ExpressService } from '../.././express.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Job } from '../../Models/Job';
import { forkJoin } from 'rxjs';
declare var jquery: any;
declare var $: any;
@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {
  public jobForm: FormGroup;
  public helpTextImg: boolean = false;
  config: any;
  jobs: Job[];

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
    

   

    let jobs = this.expressService.getJobs();
    let jobsSize = this.expressService.getJobsSize();

      forkJoin([jobsSize, jobs]).subscribe(results => {
        // results[0] is our character
        // results[1] is our character homeworld
        console.log(results[0]);
        console.log(results[1]); 
        this.config = {
          itemsPerPage: 5,
          currentPage: 1,
          totalItems: results[0]
        };
        this.jobs = results[1];
        
      });

 
  }


  ngOnInit() {

    //Allows bootstrap carousels to display 3 items per page rather than just one
    $('.carousel.carousel-multi .item').each(function () {
      var next = $(this).next();
      if (!next.length) {
        next = $(this).siblings(':first');
      }
      next.children(':first-child').clone().attr("aria-hidden", "true").appendTo($(this));

      if (next.next().length > 0) {
        next.next().children(':first-child').clone().attr("aria-hidden", "true").appendTo($(this));
      }
      else {
        $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
      }
    });
  }

  pageChanged(event) {
    this.config.currentPage = event;
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
    if (event.target.files.length > 3) {
      this.helpTextImg = true;
      return 0;
    }
    if (files) {
      while (0 !== this.jobImages.length) {
        this.jobImages.removeAt(0);
      }
      for (let file of files) {
        let reader = new FileReader();
        reader.onload = (e: any) => {
          this.jobImages.push(this.createItem({
            Img: e.target.result  //Base64 string for preview image
          }));
        }
        reader.readAsDataURL(file);
      }
      this.helpTextImg = false;
    }
  }



  submit() {
    this.jobForm.get('knowledges').setValue((<any>document.getElementById("knowledges")).value);
    this.jobForm.get('explanation').setValue((<any>document.getElementById("explanation")).value);
   

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
