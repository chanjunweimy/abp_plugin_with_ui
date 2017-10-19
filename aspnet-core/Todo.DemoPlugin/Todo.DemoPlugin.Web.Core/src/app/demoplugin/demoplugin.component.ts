import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-demoplugin',
  templateUrl: './demoplugin.component.html',
  styleUrls: ['./demoplugin.component.css']
})
export class DemoPluginComponent {
  title = 'DemoPlugin';
  x = 0;
  y = 0;
  answer = { values : 0 };

  constructor(private http: HttpClient) {}

  onKeyX(event: any) {
    this.x = Number(event.target.value);
  }

  onKeyY(event: any) {
    this.y = Number(event.target.value);
  }

  onClickAdd() {
    this.http.post('http://localhost:21021/api/services/app/CalculationService/Calculation_Add?x=' + this.x + '&y=' + this.y, {})
      .subscribe(data => {
        this.answer.values = data['result'];
      });
  }
}
