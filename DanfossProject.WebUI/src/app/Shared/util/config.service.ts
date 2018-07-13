import { Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ConfigService {

  apiURI: string;

  constructor() {
    this.apiURI = 'http://localhost:62122/api/';
  }

  getApiURI() {
    return this.apiURI;
  }

  getHeaders(): HttpHeaders {
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

      return headers;
  }
}
