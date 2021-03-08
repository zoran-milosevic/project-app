import { Component, Input, OnInit } from '@angular/core';
import { INavBarItem } from '@app/modules/shared/models/navbar.model';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  @Input() menuItems!: INavBarItem[];

  constructor() {

  }

  ngOnInit(): void {

  }

  setActiveRoute(url: string) {
    this.menuItems.map(x => x.url === url ? x.active = true : x.active = false);
  }
}
