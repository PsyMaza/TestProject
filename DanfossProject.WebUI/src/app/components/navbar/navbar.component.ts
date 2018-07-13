import {
  Component,
  OnInit,
  ElementRef
} from '@angular/core';
import {
  ROUTES
} from '../sidebar/sidebar.component';
import { FilterService } from '../../Shared/services/filter.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  value = 'Clear me';
  title = 'Домашняя';

  constructor(private filterService: FilterService) {
  }

  ngOnInit() {
  }

  search (text) {
    this.filterService.updateFilter(text);
  }
}
