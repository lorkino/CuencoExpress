import { Component, OnInit } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { User } from '../../Models/user';
import { asEnumerable } from 'linq-es2015';
@Component({
  selector: 'nav-menu-top',
  templateUrl: './nav-menu-top.component.html',
  styleUrls: ['./nav-menu-top.component.css']
})
export class NavMenuTopComponent implements OnInit {
  user: User;
  count: number; 
  constructor(private accountService: ExpressService) {
    this.user = this.getUser();
    
  } 

  ngOnInit() {
    
    console.log(this.accountService.getUser());

  }

  getUser() {
    return this.accountService.getUser();
     
  }
}
