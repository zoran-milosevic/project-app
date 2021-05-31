import { Component, OnInit } from '@angular/core';

import { NavBarService } from '@core/services';

import { INavBarItem } from '@shared/models/navbar.model';

@Component({
  selector: 'app-root',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  menuItems: INavBarItem[] = [];

  constructor(private navbarService: NavBarService) {

    this.navbarService.items.subscribe(x => this.menuItems = x);
  }

  ngOnInit(): void {
  }
}
