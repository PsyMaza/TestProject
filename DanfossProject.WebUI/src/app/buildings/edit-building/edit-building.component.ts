import {Component, OnInit, Inject, OnDestroy} from '@angular/core';
import {Validators, FormBuilder, FormGroup} from '@angular/forms';
import {Observable, Subscription} from 'rxjs';
import {BuildingRepository} from '../../Shared/repository/building.repository';
import {WaterMeterRepository} from '../../Shared/repository/waterMeter.repository';
import {WaterMeter} from '../../Shared/models/waterMeter.model';
import {map, startWith} from 'rxjs/operators';
import {BuildingUpdateModel} from '../../Shared/models/building-update.model';
import {Address} from '../../Shared/models/address.interface';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import {DialogData} from '../buildings.component';
import {Building} from '../../Shared/models/building.model';

@Component({
  selector: 'app-edit-building',
  templateUrl: './edit-building.component.html',
  styleUrls: ['./edit-building.component.css'
]})
export class EditBuildingComponent implements OnInit, OnDestroy {

  load = false;
  isLinear = false;
  errors: string;
  message: string;
  waterMeters: WaterMeter[] = [];
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  options: string[] = [];
  filteredOptions: Observable < string[] >;
  currentBuilding: Building;
  subscriptions: Subscription = new Subscription();

  constructor(
    private formBuilder: FormBuilder,
    private buildingRepository: BuildingRepository,
    private waterMeterRepository: WaterMeterRepository,
    public dialogRef: MatDialogRef <EditBuildingComponent>,
    @Inject(MAT_DIALOG_DATA)public data: DialogData
  ) {}



  ngOnInit() {

    this.subscriptions.add(
      this.waterMeterRepository
      .getWaterMeters()
      .subscribe(data => {
        this.waterMeters = data;
        data.forEach(e => this.options.push(e.SerialNumber));
      }));

    this.subscriptions.add(
      this.buildingRepository
      .getBuilding(this.data.id)
      .subscribe(e => {
        this.currentBuilding = e;
        this.load = true;
        this.initForm();
      }));
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this
      .options
      .filter(option => option.toLowerCase().includes(filterValue));
  }

  initForm() {
    this.firstFormGroup = this
      .formBuilder
      .group({
        company: [this.currentBuilding.Company],
        zip: [
          this.currentBuilding.Address.ZIPCode, Validators.required
        ],
        country: [
          this.currentBuilding.Address.Country, Validators.required
        ],
        city: [
          this.currentBuilding.Address.City, Validators.required
        ],
        street: [this.currentBuilding.Address.Street],
        building: [this.currentBuilding.Address.HouseNumber, Validators.required]
      });

    this.secondFormGroup = this
      .formBuilder
      .group({
        waterMeter: [`${this.currentBuilding.WaterMeter ? this.currentBuilding.WaterMeter.SerialNumber : ''}`]
      });

      this.filteredOptions = this
      .secondFormGroup.controls['waterMeter']
      .valueChanges
      .pipe(startWith(''), map(value => this._filter(value)));
  }

  onSubmit() {

    this.errors = '';
    this.message = '';

    const {
      company,
      zip,
      country,
      city,
      street,
      building
    } = this.firstFormGroup.value;
    const {waterMeter} = this.secondFormGroup.value;
    const address: Address = {
      ZIPCode: zip,
      Country: country,
      City: city,
      Street: street,
      HouseNumber: building
    };

    const updateBuilding: BuildingUpdateModel = {
      Id: this.currentBuilding.Id,
      Company: company,
      Address: address,
      WaterMeterId: waterMeter
        ? this
          .waterMeters
          .find(e => e.SerialNumber === waterMeter)
          .Id
        : 0
    };

    if (!this.firstFormGroup.valid) {
      this.errors = 'Форма не валидна';
    }

    this.subscriptions.add(
      this.buildingRepository
      .updateBuilding(this.currentBuilding.Id, updateBuilding)
      .subscribe(() => {
          this.message = 'Изменения сохранены.';
        }, errors => this.errors = errors.error.Message)
    );
  }

}
