import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Offer } from '../../Models/Offer';
import { ExpressService } from '../../express.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {
  public offerForm: FormGroup;
 
  config: any = { count: 0, data: [] };
  offers: Offer[];
  constructor(private expressService: ExpressService, private http: HttpClient) {


  }
   
  ngOnInit() {


    let offers = this.expressService.getOffers();
    let offersSize = this.expressService.getOffersSize();

    forkJoin([offersSize, offers]).subscribe(results => {
      // results[0] is our character
      // results[1] is our character homeworld
      console.log(results[0]);
      console.log(results[1]);
      this.config = {
        itemsPerPage: 5,
        currentPage: 1,
        totalItems: results[0]
      };
      this.offers = results[1];

    });
   
  }

  pageChanged(event) {
    this.config.currentPage = event;
  }

}
