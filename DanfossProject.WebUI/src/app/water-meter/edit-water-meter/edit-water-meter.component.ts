import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { WaterMeterRepository } from '../../Shared/repository/waterMeter.repository';
import { WaterMeterUpdateModel } from '../../Shared/models/waterMeter-update.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { WaterMeter } from '../../Shared/models/waterMeter.model';
import { DialogData } from '../../buildings/buildings.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-water-meter',
  templateUrl: './edit-water-meter.component.html',
  styleUrls: ['./edit-water-meter.component.css']
})
export class EditWaterMeterComponent implements OnInit, OnDestroy {

  load = false;
  isLinear = true;
  errors: string;
  message: string;
  firstFormGroup: FormGroup;
  currentWaterModel: WaterMeter;
  subscriptions: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    private waterMeterRepository: WaterMeterRepository,
    public dialogRef: MatDialogRef <EditWaterMeterComponent>,
    @Inject(MAT_DIALOG_DATA)public data: DialogData
  ) {}

  ngOnInit() {
    this.subscriptions = this.waterMeterRepository
    .getWaterMeter(this.data.id)
    .subscribe(data => {
      this.currentWaterModel = data;
      console.log(this.currentWaterModel);
      this.load = true;
      this.initForm();
    });
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

  initForm() {
    this.firstFormGroup = this
      .formBuilder
      .group({
        serial: [
          this.currentWaterModel.SerialNumber, Validators.required
        ],
        counterValue: [this.currentWaterModel.CounterValue, Validators.required]
      });
  }

  onSubmit() {

    this.errors = '';
    this.message = '';

    const {
      serial,
      counterValue
    } = this.firstFormGroup.value;

    const updateWaterMeter: WaterMeterUpdateModel = {
      Id: this.currentWaterModel.Id,
      SerialNumber: serial,
      CounterValue: counterValue
    };

    if (!this.firstFormGroup.valid) {
      return this.errors = 'Форма не валидна';
    }

    this
      .waterMeterRepository
      .updateWaterMeter(this.currentWaterModel.Id, updateWaterMeter)
      .subscribe(result => {
        this.message = 'Счетчик обновлен.';
      }, errors => this.errors = errors.error.Message);
  }

}
