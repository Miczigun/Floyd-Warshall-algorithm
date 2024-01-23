.data
n qword ?
it qword ?

.code
asmFloydWarshall proc
	mov n, rdx
	mov it, rbx

    xor     rbx, rbx
    xor     r8, r8


inner_loop_start:
    ; Compute indices
    mov     r10, it              ; r10 = iteration
    imul    r10, n               ; r10 = iteration * n
    add     r10, r8              ; r10 = iteration * n + next set if graph vertices is longer than 8
    shl     r10, 2

    mov     r11, rbx                ; r11 = i
    imul    r11, n                 ; r11 = i * n
    add     r11, it               ; r11 = i * n + iteration
    shl     r11, 2

    mov     r12, rbx               ; r12 = i
    imul    r12, n                 ; r12 = i * n
    add     r12, r8               ; r12 = i * n + next set if graph vertices is longer than 8
    shl     r12, 2

    vmovdqu  ymm11, ymmword ptr [rcx + r10] ;Load resultGraph[iteration * n + next set vector]

    vmovdqu  ymm12, ymmword ptr [rcx + r12] ;Load resultGraph[i * n + next set vector]

    mov r13, [rcx + r11]    ;
    movq xmm10, r13

    vpbroadcastd ymm10, xmm10
    vpaddd ymm13, ymm10, ymm11
    vpminud ymm14, ymm13, ymm12

    
    ; Update resultGraph[i * n + j]
    lea r12, [rcx + r12]
    vmovdqu ymmword ptr [r12], ymm14

    vpxor ymm14, ymm14, ymm14
    vpxor ymm13, ymm13, ymm13

    add r8, 8
    cmp r8, n
    je reset_r_eight
    jl inner_loop_start

inner_loop_increment:
    inc     rbx
    cmp     rbx, n
    je      loop_end
    jl      inner_loop_start

reset_r_eight:
    xor r8, r8
    jmp inner_loop_increment

loop_end:
	ret
    

asmFloydWarshall endp
end