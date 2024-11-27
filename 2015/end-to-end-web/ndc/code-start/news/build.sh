#!/bin/bash
if test "$OS" = "Windows_NT"
then
  MONO=""
else
  # Mono fix for https://github.com/fsharp/FAKE/issues/805
  export MONO_MANAGED_WATCHER=false
  MONO="mono"
fi

if [ ! -e ".paket/paket.exe" ]; then
  $MONO .paket/paket.bootstrapper.exe
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
fi

if [ ! -e "packages/FAKE/tools/FAKE.exe" ]; then
  if [ -e "paket.lock" ]
  then
    $MONO .paket/paket.exe restore
  else
    $MONO .paket/paket.exe install
  fi
  exit_code=$?
  if [ $exit_code -ne 0 ]; then
    exit $exit_code
  fi
fi
$MONO packages/FAKE/tools/FAKE.exe $@ --fsiargs build.fsx
