import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  element: any;

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    setInterval(
      () => (this.element = document.getElementById('myDropdown')),
      100
    );
  }

  login(): void {
    this.accountService.login(this.model).subscribe();
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/register');
  }
  reload() {
    this.accountService.reload().subscribe();
    window.location.reload();
  }
}
