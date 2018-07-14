import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { WaterMeterRepository } from '../../Shared/repository/waterMeter.repository';
import { WaterMeter } from '../../Shared/models/waterMeter.model';
import { Building } from '../../Shared/models/building.model';
import { Address } from '../../Shared/models/address.interface';
import { BuildingRepository } from '../../Shared/repository/building.repository';
import { BuildingCreateModel } from '../../Shared/models/building-create.model';
@Component({
  selector: 'app-create',
  templateUrl: './create-building.component.html',
  styleUrls: ['./create-building.component.css']
})
export class CreateBuildingComponent implements OnInit, OnDestroy {

  load = false;
  isLinear = false;
  errors: string;
  message: string;
  waterMeters: WaterMeter[] = [];
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  options: string[] = [];
  filteredOptions: Observable<string[]>;
  subscriptions: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private buildingRepository: BuildingRepository,
    private waterMeterRepository: WaterMeterRepository
  ) {}

  ngOnInit() {
    this.subscriptions =
      this.waterMeterRepository.getWaterMeters()
      .subscribe(data => {
        this.waterMeters = data;
        data.forEach(e => this.options.push(e.SerialNumber));
        this.load = true;
      });

    this.initForm();

    this.filteredOptions = this.secondFormGroup.controls['waterMeter']
      .valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  initForm() {
    this.firstFormGroup = this.formBuilder.group({
      company: [''],
      zip: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
      street: [''],
      building: ['', Validators.required],
    });

    this.secondFormGroup = this.formBuilder.group({
      waterMeter: ['']
    });
  }

  onSubmit () {

    this.errors = '';

    const { company, zip, country, city, street, building } = this.firstFormGroup.value;
    const { waterMeter } = this.secondFormGroup.value;
    const address: Address = {
      ZIPCode: zip,
      Country: country,
      City: city,
      Street: street,
      HouseNumber: building
    };

    const newBuilding: BuildingCreateModel = {
      Company: company,
      Address: address,
      WaterMeterId: waterMeter ? this.waterMeters.find(e => e.SerialNumber === waterMeter).Id : 0
    };

    if (!this.firstFormGroup.valid) {
      return this.errors = 'Форма не валидна';
    }

      this.buildingRepository.addBuilding(newBuilding)
      .subscribe(result => {
        this.message = 'Дом добавлен.';
      }, errors => this.errors = errors.error.Message);
  }

}
