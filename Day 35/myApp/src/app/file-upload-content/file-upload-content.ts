import { Component, inject } from '@angular/core';
import { BulkInsertService } from '../services/BulkInsertService';
import { HttpClient } from '@angular/common/http';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-file-upload-content',
  imports: [JsonPipe],
  templateUrl: './file-upload-content.html',
  styleUrl: './file-upload-content.css'
})
export class FileUploadContent {
  constructor(private http: HttpClient) {}
  private service =  inject(BulkInsertService);
    insertedRecords:any;

    handleFileUpload(event: any) {
    const file = event.target.files[0];
    this.service.processData(file).subscribe({
      next:(data)=>this.insertedRecords= data,
      error:(err)=>alert(err)

    })
    

  }
}
