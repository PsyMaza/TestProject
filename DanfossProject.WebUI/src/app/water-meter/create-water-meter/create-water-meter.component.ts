import {Component, OnInit} from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import {WaterMeterRepository} from '../../Shared/repository/waterMeter.repository';
import { WaterMeterCreateModel } from '../../Shared/models/waterMeter-create.model';

@Component({selector: 'app-create-water-meter',
templateUrl: './create-water-meter.component.html',
styleUrls: ['./create-water-meter.component.css']})

export class CreateWaterMeterComponent implements OnInit {

  load = false;
  isLinear = true;
  errors: string;
  message: string;
  firstFormGroup: FormGroup;

  constructor(private formBuilder: FormBuilder, private waterMeterRepository: WaterMeterRepository) {}

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.firstFormGroup = this
      .formBuilder
      .group({
        serial: [
          '', Validators.required
        ],
        counterValue: ['', Validators.required]
      });
  }

  onSubmit() {

    this.errors = '';
    this.message = '';

    const {
      serial,
      counterValue
    } = this.firstFormGroup.value;

    const newWaterMeter: WaterMeterCreateModel = {
      SerialNumber: serial,
      CounterValue: counterValue
    };

    if (!this.firstFormGroup.valid) {
      return this.errors = 'Форма не валидна';
    }

    this
      .waterMeterRepository
      .addWaterMeter(newWaterMeter)
      .subscribe(result => {
        this.message = 'Счетчик добавлен.';
      }, errors => this.errors = errors.error.Message);
  }
}
