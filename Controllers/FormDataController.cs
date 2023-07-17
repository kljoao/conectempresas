using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

[Route("api/FormData")]
[EnableCors("AllowLocalHost")]
public class FormDataController : ControllerBase
{
    private const string FilePath = "data.json";

    [HttpPost]
    [Route("")]
    public IActionResult PostFormData([FromBody] FormData formData)
    {
        try
        {
            List<FormData> formDataList;

            if (System.IO.File.Exists(FilePath))
            {
                var formDataJson = System.IO.File.ReadAllText(FilePath);
                formDataList = JsonConvert.DeserializeObject<List<FormData>>(formDataJson);
            }
            else
            {
                formDataList = new List<FormData>();
            }

            formDataList.Add(formData);

            var updatedFormDataJson = JsonConvert.SerializeObject(formDataList);
            System.IO.File.WriteAllText(FilePath, updatedFormDataJson);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

[HttpGet]
[Route("")]
public IActionResult GetFormData()
{
    try
    {
        if (System.IO.File.Exists(FilePath))
        {
            var formDataJson = System.IO.File.ReadAllText(FilePath);
            var formDataList = JsonConvert.DeserializeObject<List<FormData>>(formDataJson);

            // Ordenar a lista em ordem alfabética pelo nome
            formDataList = formDataList.OrderBy(formData => formData.Nome).ToList();

            return Ok(formDataList);
        }
        else
        {
            return NotFound();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

[HttpDelete]
[Route("{ramal}")]
public IActionResult DeleteFormData(string ramal)
{
    try
    {
        if (System.IO.File.Exists(FilePath))
        {
            var formDataJson = System.IO.File.ReadAllText(FilePath);
            var formDataList = JsonConvert.DeserializeObject<List<FormData>>(formDataJson);

            // Procura o formulário com o ramal especificado
            var formData = formDataList.FirstOrDefault(formData => formData.Ramal == Convert.ToInt32(ramal));

            if (formData != null)
            {
                formDataList.Remove(formData);

                var updatedFormDataJson = JsonConvert.SerializeObject(formDataList);
                System.IO.File.WriteAllText(FilePath, updatedFormDataJson);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return NotFound();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

[HttpPut]
[Route("{ramal}")]
public IActionResult PutFormData(string ramal, [FromBody] FormData formData)
{
    try
    {
        Console.WriteLine(JsonConvert.SerializeObject(formData));
        if (System.IO.File.Exists(FilePath))
        {
            var formDataJson = System.IO.File.ReadAllText(FilePath);
            var formDataList = JsonConvert.DeserializeObject<List<FormData>>(formDataJson);

            // Procura o formulário com o ramal especificado
            var existingFormData = formDataList.FirstOrDefault(formData => formData.Ramal == Convert.ToInt32(ramal));

            if (existingFormData != null)
            {
                // Atualiza os dados existentes com os novos dados enviados
                existingFormData.Nome = formData.Nome;
                existingFormData.Setor = formData.Setor;
                existingFormData.PA = formData.PA;
                existingFormData.Ramal = formData.Ramal;
                existingFormData.Email = formData.Email;
                existingFormData.Cell = formData.Cell;

                var updatedFormDataJson = JsonConvert.SerializeObject(formDataList);
                System.IO.File.WriteAllText(FilePath, updatedFormDataJson);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return NotFound();
        }
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}
}