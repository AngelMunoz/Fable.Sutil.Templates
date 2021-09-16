module Pages.Notes

open Browser.Types
open Sutil
open Sutil.DOM
open Sutil.Attr
open Sutil.Transition

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

let private init () = { CurrentNote = None; Notes = [] }

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
            :: state.Notes }
    | None -> state

  | Remove note ->
    { state with
        Notes = state.Notes |> List.filter (fun n -> n <> note) }
  | SetTitle title ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Title = title })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }
  | SetBody body ->
    let current =
      state.CurrentNote
      |> Option.map (fun current -> { current with Body = body })
      |> Option.orElse (Some { Id = 0; Title = ""; Body = "" })

    { state with CurrentNote = current }

let private noteTemplate (note: Note) = Html.li $"Id: {note.Id} - {note.Title}"

let Notes () =
  let state, dispatch =
    Store.makeElmishSimple init update ignore ()

  let notes = state .> (fun s -> s.Notes)

  Html.div [
    disposeOnUnmount [ state ]
    Html.form [
      on "submit" (fun _ -> dispatch Save) [ PreventDefault ]
      Html.input [
        Attr.typeText
        Attr.name "title"
        Attr.placeholder "Title"
        onKeyDown
          (fun evt ->
            SetTitle (evt.target :?> HTMLInputElement).value
            |> dispatch)
          []
        on
          "blur"
          (fun evt ->
            SetTitle (evt.target :?> HTMLInputElement).value
            |> dispatch)
          []
      ]
      Html.input [
        Attr.typeText
        Attr.name "body"
        Attr.placeholder "Body"
        onKeyDown
          (fun evt ->
            SetBody (evt.target :?> HTMLInputElement).value
            |> dispatch)
          []
        on
          "blur"
          (fun evt ->
            SetBody (evt.target :?> HTMLInputElement).value
            |> dispatch)
          []
      ]
      Html.button [
        Attr.typeSubmit
        Html.text "Add"
      ]
    ]
    Html.ul [
      eachk notes noteTemplate (fun n -> n.Id) [ InOut fade ]
    ]
  ]
