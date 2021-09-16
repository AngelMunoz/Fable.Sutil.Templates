[<RequireQualifiedAccess>]
module Pages.Home

open Lit
open Haunted


let private Home (props: {| initial: int option |}) =
  html
    $"""
        <section>
          <h3>Counter at 0</h3>
          <flit-counter></flit-counter>
        </section>
        <section>
          <h3>Counter at 100</h3>
          <flit-counter .initial={100}></flit-counter>
        </section>
        """

let register () =
  defineComponent "flit-home" (Haunted.Component Home)
