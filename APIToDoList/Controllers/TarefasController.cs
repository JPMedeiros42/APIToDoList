using APIToDoList.Models;
using Microsoft.AspNetCore.Mvc;


namespace APIToDoList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private static List<Tarefa> tarefas = new List<Tarefa>();

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(tarefas);
    }

    [HttpGet("{id:int}", Name = "ObterTarefa")]
    public IActionResult GetById(int id)
    {
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa == null)
        {
            return NotFound("Tarefa inválida");
        }
        return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Post(Tarefa tarefa)
    {
        if (tarefa is null)
        {
            return BadRequest("Tarefa inválida");
        }

        tarefa.Id = tarefas.Count > 0 ? tarefas.Max(t => t.Id) + 1 : 1; 
        tarefa.DataCriacao = DateTime.Now;
        tarefa.Status = false;
        tarefas.Add(tarefa);

        return new CreatedAtRouteResult("ObterTarefa",
            new {id = tarefa.Id}, tarefa);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Tarefa tarefa)
    {
        if (tarefa is null || tarefa.Id != id)
        {
            return BadRequest("Tarefa inválida");
        }

        var tarefaExistente = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefaExistente == null)
        {
            return NotFound("Tarefa inválida");
        }

        tarefaExistente.Titulo = tarefa.Titulo;
        tarefaExistente.Descricao = tarefa.Descricao;
        tarefaExistente.Status = tarefa.Status;

        if (tarefa.Status)
        {
            tarefaExistente.DataConclusao = DateTime.Now;
        }
        else
        {
            tarefaExistente.DataConclusao = null;
        }

        return Ok(new
        {
            mensagem = "Tarefa atualizada com sucesso",
            dados = tarefaExistente
        });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa == null)
        {
            return NotFound("Tarefa inválida");
        }

        tarefas.Remove(tarefa);
        return Ok(new
        {
            mensagem = "Tarefa excluida com sucesso",
            dados = tarefa
        });
    }
}
