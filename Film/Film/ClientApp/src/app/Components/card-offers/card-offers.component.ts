import { Component, OnInit, Input } from '@angular/core';
import { Offer } from '../../Models/Offer';

@Component({
  selector: 'app-card-offers',
  templateUrl: './card-offers.component.html',
  styleUrls: ['./card-offers.component.css']
})
export class CardOffersComponent implements OnInit {
  @Input() public offer: Offer;

  constructor() { }

  ngOnInit() {
  }

}
