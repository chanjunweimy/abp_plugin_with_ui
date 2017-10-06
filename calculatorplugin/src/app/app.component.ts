import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
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
