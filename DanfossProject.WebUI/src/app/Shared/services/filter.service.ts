import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class FilterService {

    private filter = '';

    constructor() {}

    private filterAttributesSource = new BehaviorSubject<string>(this.filter);
    filterAttribute = this.filterAttributesSource.asObservable();

    updateFilter(filter: string): void {
        this.filterAttributesSource.next(filter);
    }
}
