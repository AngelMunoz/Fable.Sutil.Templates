[<RequireQualifiedAccess>]
module Pages.Notes

open Browser.Types

open Elmish
open Lit

type private Note =
  { Id: int
    Title: string
    Body: string }

type private State =
  { CurrentNote: Note option
    Notes: Note list }

type private Msg =
  | Save
  | Remove of Note
  | SetTitle of string
  | SetBody of string

let private init () =
  { CurrentNote = None; Notes = [] }, Cmd.none

let private update msg state =
  match msg with
  | Save ->
    match state.CurrentNote
          |> Option.map
               (fun note ->
                 { note with
                     Id = (state.Notes |> Seq.length) + 1 }) with
    | Some note ->
      { state with
          Notes =
            { note with
                Id = state.Notes.Length + 1 }
            :: state.Notes },
      Cmd.none
    | None -> state, Cmd.none

  | Remove note ->
    { state with
        Notes = state.Notes |> List.filter (fun n -> n <> note) },
    Cmd.none
  | SetTitle title ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Title = title })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }, Cmd.none
  | SetBody body ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Body = body })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }, Cmd.none

let private noteTemplate note =
  html
    $"""
        <li>Id: {note.Id} - {note.Title}</li>
    """

[<LitElement("flit-notes")>]
let private Notes () =
  LitElement.init ()

  let state, dispatch = Hook.useElmish (init, update)

  let notes = state.Notes |> List.map noteTemplate

  let changeBody (evt: KeyboardEvent) = evt.target.Value |> SetBody |> dispatch

  let changeTitle (evt: KeyboardEvent) =
    evt.target.Value |> SetTitle |> dispatch

  html
    $"""
      <form @submit="{fun (ev: Event) ->
                        ev.preventDefault ()
                        dispatch Save}">
          <input
              type="text"
              name="title"
              placeholder="Title"
              @keydown={changeTitle}
              @blur={changeTitle} />
          <input
              type="text"
              name="body"
              placeholder="Body"
              @keydown={changeBody}
              @blur={changeBody}
              />
          <button type="submit">Add</button>
      </form>
      <ul>{notes}</ul>
      """

let register () = ()
