import { Component, OnInit, Input } from '@angular/core';
import { Job } from '../../Models/Job';

@Component({
  selector: 'app-card-job',
  templateUrl: './card-job.component.html',
  styleUrls: ['./card-job.component.css']
})
export class CardJobComponent implements OnInit {

  @Input() public job: Job;

  constructor() { }

  ngOnInit() {
  }

}
