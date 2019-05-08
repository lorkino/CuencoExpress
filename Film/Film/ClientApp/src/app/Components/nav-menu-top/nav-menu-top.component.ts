import { Component, OnInit } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { User } from '../../Models/user';

@Component({
  selector: 'nav-menu-top',
  templateUrl: './nav-menu-top.component.html',
  styleUrls: ['./nav-menu-top.component.css']
})
export class NavMenuTopComponent implements OnInit {
  user: User;
  constructor(private accountService: ExpressService) {
    this.user = this.getUser();
  }

  ngOnInit() {
    console.log("CASPA");
    console.log(this.accountService.getUser());

  }

  getUser() {
    return this.accountService.getUser();
  }
}
