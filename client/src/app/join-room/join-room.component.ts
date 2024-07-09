import { JsonPipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-join-room',
  templateUrl: './join-room.component.html',
  styleUrl: './join-room.component.css'
})
export class JoinRoomComponent implements OnInit{
  protected userRoomConnectionForm!: FormGroup;
  protected routerService = inject(Router);
  protected formBuilderService = inject(FormBuilder);
  protected jsonPipe = inject(JsonPipe);

  ngOnInit() {
    this.userRoomConnectionForm = this.formBuilderService.group({
      displayName: ['', Validators.required],
      roomName: ['', Validators.required]
    });
  }

  protected JoinRoom() {
    alert(this.jsonPipe.transform(this.userRoomConnectionForm.getRawValue()));
  }
}
