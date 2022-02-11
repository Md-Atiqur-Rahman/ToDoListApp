import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { NavItem } from '../models/navitem.model'
import { NavService } from '../services/nav.service';
@Component({
  selector: 'app-menu-list-item',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss'],
  animations: [
    trigger('indicatorRotate', [
      state('collapsed', style({ transform: 'rotate(0deg)' })),
      state('expanded', style({ transform: 'rotate(180deg)' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4,0.0,0.2,1)')
      ),
    ]),
  ],
})
export class MenuListComponent implements OnInit {
  expanded: boolean = true;

  constructor(public navService: NavService, public router: Router) {
    if (this.depth === undefined) {
      this.depth = 0;
    }
  }
  @HostBinding('attr.aria-expanded') ariaExpanded = this.expanded;
  @Input()
  item!: NavItem;
  @Input() depth: number = 0;
  onItemSelected(item: NavItem) {
    if (!item.children || !item.children.length) {
      this.router.navigate([item.route]);
      //this.navService.closeNav();
    }
    if (item.children && item.children.length) {
      this.expanded = !this.expanded;
    }
  }

  ngOnInit(): void {
    this.navService.currentUrl.subscribe((url: string) => {
      this.expanded = false;
      this.ariaExpanded = false;
    });
  }

}
